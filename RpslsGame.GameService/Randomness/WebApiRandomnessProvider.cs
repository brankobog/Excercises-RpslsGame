﻿namespace RpslsGame.GameService.Randomness;

using System.Text.Json.Serialization;
using ArgumentGuard = ArgumentOutOfRangeException;

public class WebRandomnessProvider(
    ILogger<WebRandomnessProvider> log,
    HttpClient httpClient) : IRandomnessProvider
{
    private const int MinRnd = 0;
    private const int MaxRnd = 100;

    public async Task<int> NextAsync(int minValue, int maxValue)
    {
        ArgumentGuard.ThrowIfNegative(minValue);
        ArgumentGuard.ThrowIfNegative(maxValue);
        ArgumentGuard.ThrowIfGreaterThanOrEqual(minValue, maxValue);

        var response = await httpClient.GetFromJsonAsync<RandomResponseDto>($"random");

        if (response is null)
        {
            // todo: log error code.
            log.LogError("Failed to get random number from the server.");
            throw new InvalidOperationException("Failed to get random number from the server.");
        }

        return NormalizeToRange(response.Value, minValue, maxValue);
    }

    public int Next(int minValue, int maxValue) =>
        NextAsync(minValue, maxValue).Result;

    public static int NormalizeToRange(int value, int rangeMin, int RangeMax) =>
        rangeMin + (value - MinRnd) * (RangeMax - rangeMin) / (MaxRnd - MinRnd);
}

public class RandomResponseDto
{
    [JsonPropertyName("random_number")]
    public int Value { get; set; }
}