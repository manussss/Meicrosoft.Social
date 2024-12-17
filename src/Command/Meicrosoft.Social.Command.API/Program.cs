var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection("MongoDbConfig"));
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection("ProducerConfig"));
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<PostAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();

var commandHandler = builder.Services.BuildServiceProvider().GetRequiredService<ICommandHandler>();
var dispatcher = new CommandDispatcher();
dispatcher.RegisterHandler<NewPostCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<EditMessageCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<LikePostCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<AddCommentCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<EditCommentCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<RemoveCommentCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<DeletePostCommand>(commandHandler.HandleAsync);
builder.Services.AddSingleton<ICommandDispatcher>(dispatcher);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapPost("api/v1/post", async (
    NewPostCommand command,
    ICommandDispatcher commandDispatcher
    ) =>
{
    var id = Guid.NewGuid();
    command.Id = id;

    await commandDispatcher.SendAsync(command);

    return Results.Created();
});

app.MapPut("api/v1/message/{id}", async (
    Guid id,
    EditMessageCommand command,
    ICommandDispatcher commandDispatcher
    ) =>
{
    command.Id = id;
    await commandDispatcher.SendAsync(command);

    return Results.Ok();
});

app.MapPut("api/v1/post/{id}", async (
    Guid id,
    ICommandDispatcher commandDispatcher
    ) =>
{
    await commandDispatcher.SendAsync(new LikePostCommand { Id = id });

    return Results.Ok();
});

app.MapPost("api/v1/comment/{id}", async (
    Guid id,
    AddCommentCommand command,
    ICommandDispatcher commandDispatcher
    ) =>
{
    command.Id = id;
    await commandDispatcher.SendAsync(command);

    return Results.Ok();
});

app.MapPut("api/v1/comment/{id}", async (
    Guid id,
    EditCommentCommand command,
    ICommandDispatcher commandDispatcher
    ) =>
{
    command.Id = id;
    await commandDispatcher.SendAsync(command);

    return Results.Ok();
});

app.Run();
