namespace ObjectWeaver.Test.CustomMappers;

public class PersonToEmployeeCustomMapper : ObjectWeaver.Lib.Interface.ICustomMapper<ObjectWeaver.Test.Models.PersonModel, ObjectWeaver.Test.Dtos.EmployeeDto>
{
    public ObjectWeaver.Test.Dtos.EmployeeDto Map(ObjectWeaver.Test.Models.PersonModel source)
    {
        return new()
        {
            EmployeeId = source.Id,
            EmployeeFirstName = source.FirstName,
            EmployeeLastName = source.LastName,
            EmployeeAge = source.Age,
            EmployeeIsActive = source.IsActive
        };
    }
}