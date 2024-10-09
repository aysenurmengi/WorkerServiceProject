// claim-> doğrulanmış kullanıcının istemci ve sunucu arasında paylaşılmak istenen bilgileri tutar.
namespace MyApi.Token
{
    //claimlecek bilgiler burda
    public class TokenRequestModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
