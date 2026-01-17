const themeColours = require('./src/theme/colours.ts');

const config = {
  content: ['./src/**/*.{js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors: {
        primary: themeColours.primary,
        secondary: themeColours.secondary,
        neutral: themeColours.neutral,
        success: themeColours.success,
        warning: themeColours.warning,
        danger: themeColours.danger,
        info: themeColours.info
      }
    }
  },
  plugins: []
};

export default config;
