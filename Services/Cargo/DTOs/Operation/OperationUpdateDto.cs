public class OperationUpdateDto
    {
        public Guid Id { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public DateTime OperationDate { get; set; }
    }