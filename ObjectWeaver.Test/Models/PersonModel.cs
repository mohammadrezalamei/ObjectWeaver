namespace ObjectWeaver.Test.Models;

public class PersonModel : System.Object
{

    public PersonModel()
    {
        
    }

    public System.Guid Id { get; set; }
    public System.String FirstName { get; set; }
    public System.String LastName { get; set; }
    public int Age { get; set; }
    public bool IsActive { get; set; }
}