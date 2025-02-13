namespace MyChat.Core.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TestA
    {
        //[Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        //[Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        public TestB TestB { get; set; } = null!;
    }
}
