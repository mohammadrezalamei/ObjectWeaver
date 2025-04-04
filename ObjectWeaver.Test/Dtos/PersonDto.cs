namespace ObjectWeaver.Test.Dtos;

public class PersonDto : System.Object
{

    public PersonDto()
    {
        
    }

    public System.Guid Id { get; set; }
    public System.String FirstName { get; set; }
    public System.String LastName { get; set; }
    public int Age { get; set; }
    public bool IsActive { get; set; }
}