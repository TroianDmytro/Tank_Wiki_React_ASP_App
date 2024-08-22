import { FC } from 'react';
import { ContainerImgWrapper } from './ContainerImg.styled';
//import imgUSSR from './Imgs/Ussr.png';

interface ContainerImgProps {
    url?:string
}

const ContainerImg: FC<ContainerImgProps> = () => (
    <ContainerImgWrapper>
        <img src="https://wiki.wgcdn.co/images/1/1b/%D0%A1%D0%A1%D0%A1%D0%A0_%D1%84%D0%BB%D0%B0%D0%B3.png" />
        <a href="#">Reference</a>
 </ContainerImgWrapper>
);

export default ContainerImg;
