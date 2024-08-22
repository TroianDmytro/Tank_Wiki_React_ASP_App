import  { FC } from 'react';
import { ContentWrapper } from './Content.styled';

interface ContentProps {
    component?: Element
}

const Content: FC<ContentProps> = () => (
 <ContentWrapper>
    Content Component
 </ContentWrapper>
);

export default Content;
