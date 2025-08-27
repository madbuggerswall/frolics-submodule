using System;
using UnityEngine;

namespace Frolics.Grids {
	public interface ICellFactory<out T, in TCoord>
		where T : CellBase<TCoord> where TCoord : struct, IEquatable<TCoord> {
		T CreateCell(TCoord coordinate, Vector3 position, float diameter);
	}
}
