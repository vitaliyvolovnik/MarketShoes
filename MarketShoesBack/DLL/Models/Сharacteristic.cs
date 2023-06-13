namespace DLL.Models
{
    public class Characteristic
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubCharacteristic> SubCharacteristics { get; set; }
    }
}
