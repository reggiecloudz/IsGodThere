using System.ComponentModel.DataAnnotations;

namespace IsGodThere.Models
{
    public class TextEditorModel
    {
        [DataType(DataType.Text)]
        public string Content { get; set; } = string.Empty;
    }
}
