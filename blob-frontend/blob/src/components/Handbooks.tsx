import { useEffect, useState } from "react";
import { fetchFiles } from "../service/handbookService";
import type { FileDto } from "../types/File";
import "../css/handbooks.css";

const API_URL = import.meta.env.VITE_API_URL as string;

const Handbooks = () => {
  const [files, setFiles] = useState<FileDto[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const loadFiles = async () => {
      try {
        const data = await fetchFiles();
        setFiles(data);
      } catch (err) {
        console.error(err);
        setError("Something went wrong");
      } finally {
        setLoading(false);
      }
    };

    loadFiles();
  }, []);

  return (
    <div className="container">
      <h1>📚 Handbooks</h1>

      {loading && <p>Loading...</p>}
      {error && <p style={{ color: "red" }}>{error}</p>}

      {!loading && files.length === 0 && !error && <p>No files found</p>}

      <div className="grid">
        {files.map((file, index) => (
          <div key={index} className="card">
            <h3 className="title">{formatFileName(file.fileName)}</h3>

            <p className="meta">{(file.size / 1024).toFixed(1)} KB</p>

            <button
              className="button"
              onClick={() =>
                window.open(`${API_URL}/${encodeURIComponent(file.fileName)}`)
              }>
              Open
            </button>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Handbooks;

function formatFileName(name: string): string {
  if (!name.includes("_")) return name;
  return name.split("_").slice(1).join("_");
}
