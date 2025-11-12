public class ColaABB
{
    private class NodoCola
    {
        public ABBTDA info;
        public NodoCola sig;
    }

    private NodoCola ini, fin;

    public void InicializarCola() { ini = fin = null; }
    public bool ColaVacia() { return ini == null; }

    public void Acolar(ABBTDA x)
    {
        NodoCola n = new NodoCola { info = x, sig = null };
        if (fin == null) ini = fin = n;
        else { fin.sig = n; fin = n; }
    }

    public void Desacolar()
    {
        if (ini == null) return;
        ini = ini.sig;
        if (ini == null) fin = null;
    }

    public ABBTDA Primero() { return ini != null ? ini.info : null; }
}
