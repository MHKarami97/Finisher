namespace Finisher.Application.Models;

public abstract record BaseNotIdCommand;

public abstract record BaseCommand(int Id) : BaseCommand<int>(Id);

public abstract record BaseCommand<T>(T Id);
