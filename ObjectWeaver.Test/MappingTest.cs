using ObjectWeaver.Lib;

namespace ObjectWeaver.Test
{
    public class MappingTest
    {
        [Fact]
        public void TestMappingTwoObjects()
        {
            ObjectWeaver.Test.Models.PersonModel model =
                new()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "MohammadReza",
                    LastName = "Lamei",
                    Age = 25,
                    IsActive = true
                };

            ObjectWeaver.Test.Dtos.PersonDto dto =
                model.MapTo<ObjectWeaver.Test.Dtos.PersonDto>();
            Assert.Equal(dto.Id, model.Id);
            Assert.Equal(dto.FirstName, model.FirstName);
            Assert.Equal(dto.LastName, model.LastName);
            Assert.Equal(dto.Age, model.Age);
            Assert.Equal(dto.IsActive, model.IsActive);
        }

        [Fact]
        public void TestCustomMapper()
        {
            ObjectWeaver.Test.Models.PersonModel model =
                new()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "MohammadReza",
                    LastName = "Lamei",
                    Age = 25,
                    IsActive = true
                };

            ObjectWeaver.Test.Dtos.EmployeeDto dto =
                model.MapTo<ObjectWeaver.Test.Models.PersonModel, ObjectWeaver.Test.Dtos.EmployeeDto>(new ObjectWeaver.Test.CustomMappers.PersonToEmployeeCustomMapper());
            Assert.Equal(dto.EmployeeId, model.Id);
            Assert.Equal(dto.EmployeeFirstName, model.FirstName);
            Assert.Equal(dto.EmployeeLastName, model.LastName);
            Assert.Equal(dto.EmployeeAge, model.Age);
            Assert.Equal(dto.EmployeeIsActive, model.IsActive);
        }
    }
}
