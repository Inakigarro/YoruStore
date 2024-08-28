import { Component } from '@angular/core';
import { AppService } from './app.service';
import { RegisterToolbar } from '@root/components/toolbar/state/toolbar.actions';
import { menuButtonClicked, pantsButtonClicked, profileButtonClicked, searchButtonClicked, shoppingCartButtonClicked, tShirtsButtonClicked } from './state/app.actions';
import { Observable } from 'rxjs';
import { Toolbar } from '@root/components/models';

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
            type: 'flat',
            label: 'Pantalones',
            icon: '',
            action: pantsButtonClicked()
          },
          {
            type: 'flat',
            label: 'Remeras',
            icon: '',
            action: tShirtsButtonClicked()
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
