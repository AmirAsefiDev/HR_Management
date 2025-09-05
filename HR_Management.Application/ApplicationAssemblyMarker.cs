namespace HR_Management.Application;

/// <summary>
///     This class is used as a marker to reference the Application assembly.
///     It allows MediatR (and other libraries) to locate and register all handlers,
///     validators, and related services defined in this layer.
///     Usage:
///     builder.Services.AddMediatR(cfg =>
///     cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly));
/// </summary>
public class ApplicationAssemblyMarker
{
}