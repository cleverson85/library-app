using System.Text.Json;

namespace Domain.Core.Contract;

public class CoreOperationRequest
{
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
