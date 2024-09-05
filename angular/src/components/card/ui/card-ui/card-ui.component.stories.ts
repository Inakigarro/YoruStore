import { Meta, moduleMetadata, StoryObj } from "@storybook/angular";
import { CardUiComponent } from "./card-ui.component";
import { CardUiModule } from "../card-ui.module";

const card: Meta<CardUiComponent> = {
    title: 'custom-component/card',
    component: CardUiComponent,
    decorators: [
        moduleMetadata({
            imports: [CardUiModule]
        })
    ]
};

export default card;
export type CardStory = StoryObj<CardUiComponent>;

export const BasicCard: CardStory = {
    
}