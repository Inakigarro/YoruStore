import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Categoria, CrearCategoria } from '@root/components/models';

@Injectable({
    providedIn: 'root'
})
export class WebApiService {
    private baseUrl = 'https://localhost:7014/';
    private itemsUrl =`${this.baseUrl}Items/`
    private categoriasUrl = `${this.baseUrl}Categorias/`

    constructor(private http: HttpClient) { }

    // Categorias.
    public crearCategoria(crearCategoria: CrearCategoria): Observable<Categoria> {
        return this.http.post<Categoria>(this.categoriasUrl + 'CrearCategoria', crearCategoria);
    }

    public obtenerCategorias() {
        return this.http.get<Categoria[]>(this.categoriasUrl + 'ObtenerCategorias');
    }

    public obtenerCategoriaById(categoriaId: string): Observable<Categoria> {
        return this.http.get<Categoria>(this.categoriasUrl + 'ObtenerCategoriaPorId?categoriaId=' + categoriaId);
    }

    // Example endpoint: POST /api/users
    createUser(user: any): Observable<any> {
        const url = `${this.baseUrl}/users`;
        return this.http.post(url, user);
    }

    // Example endpoint: GET /api/users/{id}
    getUserById(id: number): Observable<any> {
        const url = `${this.baseUrl}/users/${id}`;
        return this.http.get(url);
    }

    // Example endpoint: PUT /api/users/{id}
    updateUser(id: number, user: any): Observable<any> {
        const url = `${this.baseUrl}/users/${id}`;
        return this.http.put(url, user);
    }

    // Example endpoint: DELETE /api/users/{id}
    deleteUser(id: number): Observable<any> {
        const url = `${this.baseUrl}/users/${id}`;
        return this.http.delete(url);
    }

    // Add more endpoints as needed for your specific API

}