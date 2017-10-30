namespace Arrowgene.Ez2Off
{
    public interface IEzHandler
    {
        void Handle(EzClient client, EzPacket packet);
        int Id { get; }
    }
}
