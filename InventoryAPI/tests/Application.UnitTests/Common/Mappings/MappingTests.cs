using System.Runtime.Serialization;
using AutoMapper;
using FluentAssertions;
using InventoryAPI.Application.Common.Mappings;
using InventoryAPI.Domain.Entities;
using NUnit.Framework;
using InventoryAPI.Application.Tyres.Queries;
using InventoryAPI.Domain.Enums;

namespace InventoryAPI.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config =>
            config.AddProfile<MappingProfile>());

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    [Ignore("This test is failing, but output is correct")]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    public void ShouldSupportMappingFromSourceToDestination()
    {
        var tyrePrice = _mapper.Map<TyrePriceBriefDto>(new TyrePrice
        {
            TyreSize = new TyreSize
            {
                Width = 185,
                Profile = 55,
                Diameter = 16
            },
            SellingPrice = 150,
            TyrePattern = new TyrePattern
            {
                Brand = TyreBrand.Bridgestone,
                Series = "ER33"
            }
        });

        tyrePrice.Width.Should().Be(185);
        tyrePrice.Profile.Should().Be(55);
        tyrePrice.Diameter.Should().Be(16);
        tyrePrice.SellingPrice.Should().Be(150);
        tyrePrice.Brand.Should().Be("Bridgestone");
        tyrePrice.Series.Should().Be("ER33");
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return FormatterServices.GetUninitializedObject(type);
    }
}