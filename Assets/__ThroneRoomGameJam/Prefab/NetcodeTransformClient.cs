using Unity.Netcode.Components;


public class NetcodeTransformClient : NetworkTransform
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
