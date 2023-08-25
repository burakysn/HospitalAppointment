namespace Infrastructure.Utilities.Security.JWT
{
  public class AccessToken
  {
    public List<string> Claims { get; set; }
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
  }
}
