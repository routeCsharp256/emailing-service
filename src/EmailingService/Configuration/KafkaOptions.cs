namespace EmailingService.Configuration
{
    public class KafkaOptions
    {
        public string Servers { get; set; } = string.Empty;
        
        public string ConsumerGroup { get; set; } = string.Empty;
    }
}