import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import {
	Categoria,
	CrearCategoria,
	Item,
	Login,
	LoginResponse,
} from "@root/components/models";

@Injectable({
	providedIn: "root",
})
export class WebApiService {
	private baseUrl = "https://localhost:7014/";
	private authUrl = `${this.baseUrl}Auth/`;
	private itemsUrl = `${this.baseUrl}Items/`;
	private categoriasUrl = `${this.baseUrl}Categorias/`;

	constructor(private http: HttpClient) {}

	// Login.
	public login(login: Login) {
		return this.http.post<LoginResponse>(this.authUrl + "Login", login);
	}

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

	public obtenerCategoriaPorNombre(nombre: string): Observable<Categoria> {
		return this.http.get<Categoria>(
			this.categoriasUrl + "ObtenerCategoriaPorNombre?nombre=" + nombre
		);
	}

	// Items.
	public obtenerItemPorId(itemId: string) {
		return this.http.get<Item>(
			this.itemsUrl + "ObtenerItemPorId?itemId=" + itemId
		);
	}

	public obtenerItemsPorFiltro(categoriaId: string, filter: string) {
		return this.http.get<Item[]>(
			this.itemsUrl +
				"ObtenerItemsPorFiltro" +
				"?categoriaId=" +
				categoriaId +
				"&filter=" +
				filter
		);
	}
}
