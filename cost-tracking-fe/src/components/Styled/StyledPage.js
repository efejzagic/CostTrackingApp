// StyledPage.js
import React from 'react';
import styled from 'styled-components';
import background from '../../static/img/backround_image.jpg';
import Nav from '../Nav/Nav';
const StyledPageContainer = styled.div`
  background-image: url(${background});
  background-size: cover;
  background-position: center;
  height: 100vh;
  position: relative;

  &::before {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: linear-gradient(to bottom, #b3e0ff 0%, #5cacee 100%);
    opacity: 0;
  }
`;

const StyledPage = ({ children }) => {
  return <StyledPageContainer><Nav/>{children}</StyledPageContainer>;
};

export default StyledPage;
