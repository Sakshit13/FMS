using System.ComponentModel.DataAnnotations;

namespace COMMON
{
    public class GResposnse
    {
        [Key]
        public int Result { get; set; }
        public string Msg { get; set; }
        public string Code { get; set; }
        public string Data { get; set; }
    }
}
