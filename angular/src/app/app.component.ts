import { Component } from '@angular/core';
import { AppService } from './app.service';
import { RegisterToolbar } from '@root/components/toolbar/state/toolbar.actions';
import { menuButtonClicked, pantsButtonClicked, profileButtonClicked, searchButtonClicked, SecondaryToolbarActions, shoppingCartButtonClicked, tShirtsButtonClicked } from './state/app.actions';
import { Observable, of } from 'rxjs';
import { Item, Toolbar } from '@root/components/models';

const MAIN_TOOLBAR_ID = 'app-main-toolbar';
const SECONDARY_TOOLBAR_ID = 'app-secondary-toolbar';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  public mainToolbar$: Observable<Toolbar | undefined>;
  public secondaryToolbar$: Observable<Toolbar | undefined>;
  public isMenuOpened$: Observable<boolean>;

  public data$: Observable<Item[]> = of([
    {
      id: '1',
      titulo: 'Medias Negras Vlack',
      descripcion: 'Unas medias buenisimas para jugar al hockey',
      precio: 10000
    },
    {
      id: '2',
      titulo: 'Medias Negras Vlack',
      descripcion: 'Unas medias buenisimas para jugar al hockey',
      precio: 10000
    },
    {
      id: '2',
      titulo: 'Medias Negras Vlack',
      descripcion: 'Unas medias buenisimas para jugar al hockey',
      precio: 10000
    },
    {
      id: '2',
      titulo: 'Medias Negras Vlack',
      descripcion: 'Unas medias buenisimas para jugar al hockey',
      precio: 10000
    },
    {
      id: '2',
      titulo: 'Medias Negras Vlack',
      descripcion: 'Unas medias buenisimas para jugar al hockey',
      precio: 10000
    }, {
      id: '2',
      titulo: 'Medias Negras Vlack',
      descripcion: 'Unas medias buenisimas para jugar al hockey',
      precio: 10000
    }])

  constructor(private service: AppService) {
    this.service.dispatch(RegisterToolbar({
      toolbar: {
        id: MAIN_TOOLBAR_ID,
        mainButton: {
          type: 'icon',
          icon: 'menu',
          label: '',
          action:  menuButtonClicked()
        },
        title: 'Yoru Store',
        secondaryButton: [
          {
            type: 'basic',
            icon: 'search',
            label: 'Buscar',
            action: searchButtonClicked()
          },
          {
            type: 'icon',
            icon: 'shopping_cart',
            label: '',
            action: shoppingCartButtonClicked()
          },
          {
            type: 'icon',
            icon: 'account_circle',
            label: '',
            action: profileButtonClicked()
          }
        ],
        toolbarConfig: {
          isTitleSeparete: true,
          isSecondaryToolbar: false
        }
      }
    }));
    this.service.dispatch(RegisterToolbar({
      toolbar: {
        id: SECONDARY_TOOLBAR_ID,
        secondaryButton: [
          {
            type: 'fab',
            label: 'Medias',
            icon: '',
            action: SecondaryToolbarActions.buttonClicked({categoria: 'Medias'}),
          },
          {
            type: 'fab',
            label: 'Pantalones',
            icon: '',
            action: SecondaryToolbarActions.buttonClicked({categoria: 'pantalones'}),
          },
          {
            type: 'fab',
            label: 'Remeras',
            icon: '',
            action: SecondaryToolbarActions.buttonClicked({categoria: 'Remeras'}),
          }
        ],
        toolbarConfig:{
          isSecondaryToolbar:true,
          isTitleSeparete: false
        }
      }
    }))
    this.mainToolbar$ = this.service.getToolbarById(MAIN_TOOLBAR_ID);
    this.secondaryToolbar$ = this.service.getToolbarById(SECONDARY_TOOLBAR_ID);
    this.isMenuOpened$ = this.service.isMenuOpened$;
  }
  title = 'angular';
}
