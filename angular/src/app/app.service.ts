import { Injectable } from "@angular/core";
import { Action, Store } from "@ngrx/store";
import { selectAllToolbars } from "@root/components/toolbar/state/toolbar.selectors";
import { filter, map, Observable } from "rxjs";
import { getMenuOpened } from "./state/app.selectors";
import { WebApiService } from "./web-api.service";

@Injectable({
    providedIn: 'root'
})
export class AppService {
    public isMenuOpened$: Observable<boolean>;
    constructor(
        private store: Store,
        private webApi: WebApiService){
        this.isMenuOpened$ = this.store.select(getMenuOpened);
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
}