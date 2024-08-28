import { Meta, moduleMetadata, StoryObj} from '@storybook/angular';
import { ButtonComponent } from './button.component';
import { AppButtonModule } from './button.module';

const Button: Meta<ButtonComponent> = {
    title: 'custom-component/button',
    component: ButtonComponent,
    decorators: [
        moduleMetadata({
            imports: [AppButtonModule]
        })
    ],
};

export default Button;
export type ButtonStory = StoryObj<ButtonComponent>;

export const BasicButtonWithLabelOnly : ButtonStory = {
    args: {
        type: 'basic',
        label: 'Aceptar',
    }
}

export const BasicButtonWithIconOnly : ButtonStory = {
    args: {
        type: 'basic',
        icon: 'home',
    }
}

export const ButtonWithLabelAndIcon : ButtonStory = {
    args: {
        type: 'raised',
        label: 'Inicio',
        icon: 'home,'
    }
}

export const FabButtonWithIconOnly : ButtonStory = {
    args: {
        type: 'fab',
        icon: 'home'
    }
}

export const IconButtonWithIconOnly : ButtonStory = {
    args: {
        type: 'icon',
        icon: 'home'
    }
}