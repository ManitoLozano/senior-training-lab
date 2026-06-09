namespace Sales.Domain.Customers;

public sealed class Customer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Document { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Customer(string name, string email, string document)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Document = document;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string email)
    {
        Rename(name);
        ChangeEmail(email);
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Name cannot be empty");
        
        Name = name;
    }

    public void ChangeEmail(string newEmail)
    {
        if(string.IsNullOrWhiteSpace(newEmail))
            throw new Exception("Email cannot be empty");
        
        if(!newEmail.Contains("@"))
            throw new Exception("Invalid email format");
        
        Email = newEmail.Trim().ToLower();
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Desactivate()
    {
        IsActive = false;
    }
    
    public void SetDocument(string document)
    {
        if (string.IsNullOrWhiteSpace(document))
            throw new Exception("Document cannot be empty");
        
        Document = OnlyNumbers(document);
    }
    
    private static string OnlyNumbers(string value)
    {
        return new string(value.Where(char.IsDigit).ToArray());
    }
}