import { Injectable } from "@angular/core";
import { Action, Store } from "@ngrx/store";
import { selectAllToolbars } from "@root/components/toolbar/state/toolbar.selectors";
import { filter, map, Observable } from "rxjs";
import { getCurrentCategory, getCurrentCategoryItems, getCurrentCategoryName, getCurrentUserProfile, getMenuOpened } from "./state/app.selectors";
import { WebApiService } from "./web-api.service";
import { UserProfile } from "./identity/models";
import { Categoria, Item } from "@root/components/models";

@Injectable({
    providedIn: 'root'
})
export class AppService {
    public isMenuOpened$: Observable<boolean>;
    public currentUserProfile$: Observable<UserProfile | undefined>;
    public currentCategory$: Observable<Categoria | undefined>;
    public currentCategoryName$: Observable<string>;
    public currentCategoryItems$: Observable<Item[]>;

    constructor(
        private store: Store,
        private webApi: WebApiService){
        this.isMenuOpened$ = this.store.select(getMenuOpened);
        this.currentUserProfile$ = this.store.select(getCurrentUserProfile);
        this.currentCategory$ = this.store.select(getCurrentCategory);
        this.currentCategoryName$ = this.store.select(getCurrentCategoryName);
        this.currentCategoryItems$ = this.store.select(getCurrentCategoryItems);
    }

    public dispatch(action: Action){
        this.store.dispatch(action);
    }

    public getToolbarById(id: string) {
        return this.store.select(selectAllToolbars).pipe(
            map(toolbars => toolbars.find(t => t.id == id))
        )
    }

    public crearCategoria(nombre: string){
        this.webApi
            .crearCategoria({nombre: nombre})
            .pipe(filter(x => !!x))
            .subscribe(categoria => console.log(categoria));
    }
    public ObtenerCategorias() {
        return this.webApi
            .obtenerCategorias()
            .pipe(filter(x => !!x));
    }
    public ObtenerCategoriaPorId(categoriaId: string) {
        return this.webApi
            .obtenerCategoriaById(categoriaId)
            .pipe(filter(x => !!x));
    }
}