namespace SW_File_Helper.DAL.Models
{
    public class ModelBase : IEquatable<ModelBase>
    {
        public string TypeName { get; set; }

        public Guid Id { get; set; }

        public bool Equals(ModelBase? other)
        {
            if(other == null) return false;

            return Id == other.Id;
        }
    }
}
