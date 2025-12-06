using System.Runtime.CompilerServices;
using Mapster;
using MapsterMapper;
using Finisher.Application.Business.Identity.Queries;
using Finisher.Domain.Business.User.Entities;
using NUnit.Framework;

namespace Finisher.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    // private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        var config = new TypeAdapterConfig();
        config.Scan(AppDomain.CurrentDomain.GetAssemblies());

        TypeAdapterConfig.GlobalSettings.Scan(AppDomain.CurrentDomain.GetAssemblies());

        _mapper = new Mapper(config);
        // _configuration = new MapperConfiguration(config => 
        //     config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));
        //
        // _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        // _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(User), typeof(UserDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private static object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
        {
            return Activator.CreateInstance(type)!;
        }

        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
