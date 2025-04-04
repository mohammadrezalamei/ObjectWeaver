namespace ObjectWeaver.Test.Dtos;

public class EmployeeDto : System.Object
{

    public EmployeeDto()
    {
        
    }

    public System.Guid EmployeeId { get; set; }
    public System.String EmployeeFirstName { get; set; }
    public System.String EmployeeLastName { get; set; }
    public int EmployeeAge { get; set; }
    public bool EmployeeIsActive { get; set; }
    public System.String FullName => $"{EmployeeFirstName} {EmployeeLastName}";
}