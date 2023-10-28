import React from 'react';
import { css } from "@emotion/react";
import { RingLoader } from "react-spinners";



const LoadingCoomponent = ({ loading }) => {
 
  const loadingSpinnerContainerStyles = css`
  display: flex;
  justify-content: center; 
  align-items: center; 
  width: 100%;
  height: 100vh; 
`;

const override = css`
  display: block;
  margin: 0 auto;
  border-color: red;
`;

  return (
    <div css={loadingSpinnerContainerStyles}>
      <RingLoader color="#007BFF" loading={loading} css={override} size={75} />
    </div>
  );
};

export default LoadingCoomponent;
