using RaceDay.API.Models; using RaceDay.API.Services;
namespace RaceDay.API.Tests;
public class PasswordServiceTests
{
 [Fact] public void Hash_IsNotPlainText_AndVerifiesCorrectPassword(){var svc=new PasswordService();var user=new User{Email="test@example.com"};var hash=svc.Hash(user,"SecurePassword123!");Assert.NotEqual("SecurePassword123!",hash);Assert.True(svc.Verify(user,hash,"SecurePassword123!"));}
 [Fact] public void Verify_RejectsWrongPassword(){var svc=new PasswordService();var user=new User{Email="test@example.com"};var hash=svc.Hash(user,"SecurePassword123!");Assert.False(svc.Verify(user,hash,"wrong-password"));}
}
