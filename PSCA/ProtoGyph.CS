public class ProtoGyph
{
    public enum Fini : int
    {
        Cal = '"',
        Nal = Cal
    }
    public enum Lexi : int
    {
        Cal = '{',
        Nal = '}'
    }
    public enum Ordi : int
    {
        Cal = '[',
        Nal = ']'
    }
    public enum Func : int
    {
        Tion = '.',
        Tor = '\0',
    }
    public enum Equa : int
    {
        Tion = ':',
        Tor = '=',
    }
    public enum Separa : int
    {
        Tion = ',',
        Tor = ';',
    }
}
