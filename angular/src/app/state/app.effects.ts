import { Injectable } from "@angular/core";
import { AppService } from "../app.service";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { SecondaryToolbarActions } from './app.actions';
import { filter, tap } from "rxjs";

@Injectable()
export class AppEffects {
    public secondaryButtonClicked$ = createEffect(() =>
        this.actions.pipe(
            ofType(SecondaryToolbarActions.buttonClicked),
            tap(action => this.service
                .ObtenerCategoriaPorId(action.categoriaId)
                .pipe(filter(x => !!x))
                .subscribe(categoria => 
                    this.service.dispatch(
                        SecondaryToolbarActions.categoriaCargada({
                            categoria
                        })
                    )
                )
            )
        ), {dispatch: false});

    constructor(
        private actions: Actions,
        private readonly service: AppService){}
}