﻿using System.Text.Json;

namespace Application.Core.Contract;

public class CoreOperationRequest
{
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
