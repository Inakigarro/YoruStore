import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Categoria, CrearCategoria, Item } from "@root/components/models";

@Injectable({
	providedIn: "root",
})
export class WebApiService {
	private baseUrl = "https://localhost:7014/";
	private itemsUrl = `${this.baseUrl}Items/`;
	private categoriasUrl = `${this.baseUrl}Categorias/`;

	constructor(private http: HttpClient) {}

	// Categorias.
	public crearCategoria(crearCategoria: CrearCategoria): Observable<Categoria> {
		return this.http.post<Categoria>(
			this.categoriasUrl + "CrearCategoria",
			crearCategoria
		);
	}

	public obtenerCategorias() {
		return this.http.get<Categoria[]>(this.categoriasUrl + "ObtenerCategorias");
	}

	public obtenerCategoriaById(categoriaId: string): Observable<Categoria> {
		return this.http.get<Categoria>(
			this.categoriasUrl + "ObtenerCategoriaPorId?categoriaId=" + categoriaId
		);
	}

	// Items.
	public obtenerItemPorId(itemId: string) {
		return this.http.get<Item>(
			this.itemsUrl + "ObtenerItemPorId?itemId=" + itemId
		);
	}
}
