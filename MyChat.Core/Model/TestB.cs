namespace MyChat.Core.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TestB
    {
        //[Key]
        public int Id { get; set; }

        public string Detail { get; set; } = null!;

        public int TestAId { get; set; }

        //[ForeignKey(nameof(TestAId))]
        public TestA TestA { get; set; } = null!;
    }
}

