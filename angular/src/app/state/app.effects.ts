import { Injectable } from "@angular/core";
import { AppService } from "../app.service";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { categoriesObtained, InitApp, SecondaryToolbarActions } from './app.actions';
import { filter, map, switchMap } from "rxjs";
import { Button } from "@root/components/models";
import { RegisterToolbar } from "@root/components/toolbar/state/toolbar.actions";
import { SECONDARY_TOOLBAR_ID } from "../app-constants";

@Injectable()
export class AppEffects {
    public initCategorias$ = createEffect(() =>
        this.actions.pipe(
            ofType(InitApp),
            switchMap(() => this.service.ObtenerCategorias()),
            filter(categorias => !!categorias),
            map(categorias => categoriesObtained({categorias}))
            ));

    public registerSecondaryToolbar$ = createEffect(() =>
        this.actions.pipe(
            ofType(categoriesObtained),
            filter(({categorias}) => !!categorias),
            map(({categorias}) => {
                let secondaryButtons: Button[] = [];
                categorias.forEach(cat => {
                    let button : Button = {
                        type: 'fab',
                        label: cat.nombre,
                        icon: '',
                        action: SecondaryToolbarActions.buttonClicked({categoriaId: cat.id})
                      };
                      secondaryButtons.push(button);
                });
                return RegisterToolbar({
                    toolbar: {
                        id: SECONDARY_TOOLBAR_ID,
                        secondaryButton: secondaryButtons,
                        toolbarConfig:{
                          isSecondaryToolbar:true,
                          isTitleSeparete: false
                        }
                      }
                })
            })
        ));

    public setFirstCategoryAsCurrent$ = createEffect(() =>
        this.actions.pipe(
            ofType(categoriesObtained),
            filter(({categorias}) => !!categorias),
            map(({categorias}) => SecondaryToolbarActions.buttonClicked({
                categoriaId: categorias[0].id
            }))
        ));

    public secondaryButtonClicked$ = createEffect(() =>
        this.actions.pipe(
            ofType(SecondaryToolbarActions.buttonClicked),
            switchMap(action => this.service.ObtenerCategoriaPorId(action.categoriaId)),
            filter(categoria => !!categoria),
            map((categoria) => SecondaryToolbarActions.categoriaCargada({categoria})),
        ));

    constructor(
        private actions: Actions,
        private readonly service: AppService){}
}