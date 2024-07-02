namespace Application.Books.Validators.Books;

public class BookCreateValidator : BookValidator
{
    public BookCreateValidator()
    {
        ValidateAuthor();
        ValidateTitle();
        ValidateRegisterNumber();
    }
}
