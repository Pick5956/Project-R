using UnityEngine;

public class CellData3D : MonoBehaviour
{
    protected Vector3 m_Pos; //ตำแหน่งปัจจุบัน
    protected Vector3 m_BasePos; //ตำแหน่งเริ่มต้น

    public virtual void Init(Vector3 coord)
    {
        m_Pos = coord;
        m_BasePos = coord;
    }

}
