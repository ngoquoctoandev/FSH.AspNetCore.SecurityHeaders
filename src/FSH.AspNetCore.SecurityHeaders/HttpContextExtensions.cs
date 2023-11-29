using FSH.AspNetCore.SecurityHeaders.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace FSH.AspNetCore.SecurityHeaders;

/// <summary>
///     Helper methods for working with nonces
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    ///     Fetch the nonce (number used once) for the request
    /// </summary>
    /// <param name="context">The <see cref="HttpContext" /> for the request</param>
    /// <returns>The nonce for the request, as a string</returns>
    public static string GetNonce(this HttpContext context) => context.Items[Constants.DefaultNonceKey] as string ?? string.Empty;

    /// <summary>
    ///     Fetch the nonce (number used once) for the request
    /// </summary>
    /// <param name="context">The <see cref="HttpContext" /> for the request</param>
    /// <param name="nonce">The nonce to set for the request</param>
    internal static void SetNonce(this HttpContext context, string nonce)
    {
        if (string.IsNullOrEmpty(nonce)) throw new ArgumentException("Value cannot be null or empty.", nameof(nonce));

        context.Items[Constants.DefaultNonceKey] = nonce;
    }

    /// <summary>
    ///     Adds a CSP hash to the collection of sources for script-src
    /// </summary>
    /// <param name="context">The <see cref="HttpContext" /> for the request</param>
    /// <param name="algorithm">The algorithm used to calcualte the hash</param>
    /// <param name="hash">The hash generated from the content</param>
    public static void SetScriptCSPHash(this HttpContext context, CSPHashType algorithm, string hash)
    {
        SetHash(context, Constants.DefaultScriptHashKey, algorithm, hash);
    }

    /// <summary>
    ///     Adds a CSP hash to the collection of sources for style-src
    /// </summary>
    /// <param name="context">The <see cref="HttpContext" /> for the request</param>
    /// <param name="algorithm">The algorithm used to calcualte the hash</param>
    /// <param name="hash">The hash generated from the content</param>
    public static void SetStylesCSPHash(this HttpContext context, CSPHashType algorithm, string hash)
    {
        SetHash(context, Constants.DefaultStylesHashKey, algorithm, hash);
    }

    /// <summary>
    ///     Get the CSP hashes  for script-src
    /// </summary>
    /// <param name="context">The <see cref="HttpContext" /> for the request</param>
    /// <returns>The </returns>
    internal static IEnumerable<string> GetScriptCSPHashes(this HttpContext context) => context.Items[Constants.DefaultScriptHashKey] as List<string> ?? Enumerable.Empty<string>();

    /// <summary>
    ///     Get the CSP hashes  for style-src
    /// </summary>
    /// <param name="context">The <see cref="HttpContext" /> for the request</param>
    /// <returns>The </returns>
    internal static IEnumerable<string> GetStyleCSPHashes(this HttpContext context) => context.Items[Constants.DefaultStylesHashKey] as List<string> ?? Enumerable.Empty<string>();

    private static void SetHash(HttpContext context, string key, CSPHashType algorithm, string hash)
    {
        if (context.Items[key] is not List<string> hashes)
        {
            hashes             = new List<string>();
            context.Items[key] = hashes;
        }

        hashes.Add(GetSource(algorithm, hash));
    }

    private static string GetSource(CSPHashType algorithm, string hash)
    {
        return algorithm switch
        {
            CSPHashType.SHA256 => $"'sha256-{hash}'",
            CSPHashType.SHA384 => $"'sha384-{hash}'",
            CSPHashType.SHA512 => $"'sha512-{hash}'",
            _                  => throw new InvalidOperationException($"Unknown CSP hash algorithm: {algorithm}")
        };
    }
}