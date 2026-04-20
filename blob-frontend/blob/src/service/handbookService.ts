import type { FileDto } from "../types/File";

/*
 ============================================================
 API configuration

 Replace VITE_API_URL in .env with your backend URL
 ============================================================
*/

const API_URL = import.meta.env.VITE_API_URL as string;

export async function fetchFiles(): Promise<FileDto[]> {
  const res = await fetch(API_URL);

  if (!res.ok) {
    throw new Error("Failed to fetch files");
  }

  return await res.json();
}
