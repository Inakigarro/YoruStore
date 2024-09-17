import { Component } from '@angular/core';
import { AppService } from './app.service';
import { RegisterToolbar } from '@root/components/toolbar/state/toolbar.actions';
import { menuButtonClicked, profileButtonClicked, searchButtonClicked, SecondaryToolbarActions, shoppingCartButtonClicked, userProfileObtained } from './state/app.actions';
import { Observable, of } from 'rxjs';
import { Button, Item, Toolbar } from '@root/components/models';
import { UserProfile } from './identity/models';

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
  public currentUserProfile$: Observable<UserProfile | undefined>;

  public isAdmin: boolean = true;

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
    this.service
      .ObtenerCategorias()
      .subscribe(categorias => {
        let botonesCategorias: Button[] = [];
        categorias.forEach(cat => {
          let button : Button = {
            type: 'fab',
            label: cat.nombre,
            icon: '',
            action: SecondaryToolbarActions.buttonClicked({categoriaId: cat.id})
          };
          botonesCategorias.push(button);
        });
        this.service.dispatch(RegisterToolbar({
          toolbar: {
            id: SECONDARY_TOOLBAR_ID,
            secondaryButton: botonesCategorias,
            toolbarConfig:{
              isSecondaryToolbar:true,
              isTitleSeparete: false
            }
          }
        }))
      });
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
    this.service.dispatch(userProfileObtained({
      userProfile: {
        id: '1',
        loginId: 'Zaky',
        nombre: 'Iñaki',
        apellido: 'Garro',
        email: 'email@email.com'
      }
    }))
    this.mainToolbar$ = this.service.getToolbarById(MAIN_TOOLBAR_ID);
    this.secondaryToolbar$ = this.service.getToolbarById(SECONDARY_TOOLBAR_ID);
    this.isMenuOpened$ = this.service.isMenuOpened$;
    this.currentUserProfile$ = this.service.currentUserProfile$;
  }
  title = 'angular';
}
