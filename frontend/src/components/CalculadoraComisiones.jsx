import { useState } from "react";
import "./CalculadoraComisiones.css";

export default function CalculadoraComisiones() {
  const [fechaInicio, setFechaInicio] = useState("");
  const [fechaFin, setFechaFin] = useState("");
  const [resultados, setResultados] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const calcularComisiones = async () => {
    if (!fechaInicio || !fechaFin) {
      alert("Por favor selecciona ambas fechas.");
      return;
    }

    setLoading(true);
    setError(null);

    try {
      // Petición HTTP GET al endpoint de comisiones
      const response = await fetch(
        `http://localhost:5172/api/Comisiones?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`
      );
      
      if (!response.ok) {
        throw new Error("Error en la petición: " + response.statusText);
      }
      
      const data = await response.json();
      setResultados(data);
    } catch (err) {
      console.error(err);
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="calculadora-container">
      <h2>Cálculo de Comisiones</h2>
      <div className="formulario">
        <div className="campo">
          <label>Fecha Inicio</label>
          <input
            type="date"
            value={fechaInicio}
            onChange={(e) => setFechaInicio(e.target.value)}
          />
        </div>
        <div className="campo">
          <label>Fecha Fin</label>
          <input
            type="date"
            value={fechaFin}
            onChange={(e) => setFechaFin(e.target.value)}
          />
        </div>
        <button onClick={calcularComisiones} disabled={loading}>
          {loading ? "Calculando..." : "Calcular"}
        </button>
      </div>

      {error && <div className="error-mensaje">{error}</div>}

      <div className="tabla-container">
        <table>
          <thead>
            <tr>
              <th>Vendedor</th>
              <th>Total Ventas</th>
              <th>Comisión</th>
            </tr>
          </thead>
          <tbody>
            {resultados.length === 0 && !loading && (
              <tr>
                <td colSpan="3" style={{ textAlign: "center" }}>
                  Sin datos o selecciona un rango de fechas.
                </td>
              </tr>
            )}
            {resultados.map((res, index) => (
              <tr key={index}>
                {/* Mapeo de datos del reporte */}
                <td>{res.vendedor}</td>
                <td>${res.totalVentas?.toFixed(2)}</td>
                <td>${res.comision?.toFixed(2)}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
