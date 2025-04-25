using Microsoft.AspNetCore.Mvc;
using roadconstruction_apis.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;


namespace roadconstruction_apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoadConstructionController : ControllerBase
    {
        private readonly string filePath = "Data/data.json";

        [HttpGet("Get")]
        public IActionResult Get()
        {
            if (System.IO.File.Exists(filePath))
            {
                var json = System.IO.File.ReadAllText(filePath);
                var data = JsonSerializer.Deserialize<RoadConstruction>(json);

                return data != null ? Ok(data) : NotFound("Data not found.");
            }

            return NotFound("File not found.");
        }


        [HttpPost("update")]
        public IActionResult update([FromBody] Data input)
        {
            if (!System.IO.File.Exists(filePath))
                return NotFound("JSON file not found.");

            var json = System.IO.File.ReadAllText(filePath);
            var existingData = JsonSerializer.Deserialize<RoadConstruction>(json);

            var match = existingData.Datas.FirstOrDefault(d => d.SamplingTime == input.SamplingTime);

            if (match != null)
            {
                match.Properties = input.Properties;
            }
            else
            {
                existingData.Datas.Add(input);
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            string updatedJson = JsonSerializer.Serialize(existingData, options);
            System.IO.File.WriteAllText(filePath, updatedJson);

            return Ok(new { message = "Data Updated successfully." });
        }
    
    }
}
