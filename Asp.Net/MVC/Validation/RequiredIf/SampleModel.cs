public class SampleModel
    {
    // Type dropdown
    //Based on selected type value Text box1 and 2 will become reqired fields.
    public int Type { get; set; }
    
    [RequiredIf("Type", 1)]
    public int? TextBox1 { get; set; }
    
    [RequiredIf("Type", 2)]
    public int? TextBox2 { get; set; }
    }
