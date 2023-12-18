namespace Entities.RequestFeatures
{
    public class ApartmentParameters : RequestFeatures
    {
        public uint MinNumberRoom { get; set; }
        public uint MaxNumberRoom { get; set; } = 99;
        public bool ValidNumberRoomRange => MaxNumberRoom > MinNumberRoom;
    }
}