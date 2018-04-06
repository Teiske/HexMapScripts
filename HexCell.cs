using UnityEngine;

public class HexCell : MonoBehaviour {
    [SerializeField]
    HexCell[] neighbors;

    public HexCoordinates coordinates;

	public Color color;

	public RectTransform uiRect;

    int elevation;

    public int Elevation {
		get {
			return elevation;
		}
		set {
			elevation = value;
			Vector3 position = transform.localPosition;
			position.y = value * HexMetrics.elevationStep;
            position.y += (HexMetrics.SampleNoise(position).y * 2f - 2f) * HexMetrics.elevationPerturbStrength;
			transform.localPosition = position;

			Vector3 uiPosition = uiRect.localPosition;
            uiPosition.z = -position.y;
			uiRect.localPosition = uiPosition;
		}
	}

    public Vector3 Position {
        get {
            return transform.localPosition;
        }
    }

    public HexCell GetNeighbor (HexDirection direction) {
		return neighbors[(int)direction];
	}

    public void SetNeighbor(HexDirection direction, HexCell cell) {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }

    public HexEdgeType GetEdgeType(HexDirection direction) {
        return HexMetrics.GetEdgeType(elevation, neighbors[(int)direction].elevation);
    }

    public HexEdgeType GetEdgeType(HexCell otherCell) {
        return HexMetrics.GetEdgeType(elevation, otherCell.elevation);
    }

}
